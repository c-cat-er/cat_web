document.addEventListener('DOMContentLoaded', function () {
    const swaggerApiUrl = document.getElementById('registrationForm').dataset.swaggerApiUrl;

    function initializeFlatpickr(selector, config) {
        return flatpickr(selector, config);
    }

    function initializeDataTable(selector) {
        return $(selector).DataTable({
            paging: true,
            searching: false,
            lengthChange: false,
            pageLength: 10,
            responsive: true,
            language: {
                url: "https://cdn.datatables.net/plug-ins/2.0.8/i18n/zh-HANT.json",
                emptyTable: "沒有預約紀錄"
            },
            columnDefs: [
                { orderable: false, targets: [2, 9, 10, 11] },
                { className: 'dt-left', targets: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9] },
                { className: 'dt-right', targets: [10, 11] },
                { className: 'dt-center', targets: [12] },
                { width: '15%', targets: 3 },
                { width: '10%', targets: 4 }
            ],
            order: [[5, 'asc']]
        });
    }

    const datepickerConfig3 = {
        altInput: true,
        dateFormat: "Y-m-d",
        altFormat: "Y年m月d日",
        allowInput: true,
        locale: {
            weekdays: {
                shorthand: ["日", "一", "二", "三", "四", "五", "六"],
                longhand: ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"],
            },
            months: {
                shorthand: ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月"],
                longhand: ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月"],
            },
            firstDayOfWeek: 1,
            time_24hr: true,
            scrollTitle: "滾動切換",
            confirmText: "OK",
            weekNumbers: true,
            minDate: new Date().fp_incr(-60),
            maxDate: new Date().fp_incr(60)
        }
    };

    const datepicker3 = initializeFlatpickr("#datetimepicker3", datepickerConfig3);
    const registrationTable = initializeDataTable('#registrationTable');

    function convertAcuteToNumber(acute) {
        switch (acute) {
            case '看診':
                return '1';
            case '療程':
                return '2';
            case '諮詢':
                return '3';
            case '手術':
                return '4';
            default:
                return '';
        }
    }

    function convertAcuteToValue(acute) {
        switch (acute) {
            case '1':
                return { display: '看診', className: 'acute-medical' };
            case '2':
                return { display: '療程', className: 'acute-therapy' };
            case '3':
                return { display: '諮詢', className: 'acute-consultation' };
            case '4':
                return { display: '手術', className: 'acute-surgery' };
            default:
                return { display: '無', className: '' };
        }
    }

    function getCurrentROCYear() {
        const currentDate = new Date();
        const currentYear = currentDate.getFullYear();
        const rocYear = currentYear - 1911;
        const month = String(currentDate.getMonth() + 1).padStart(2, '0');
        const day = String(currentDate.getDate()).padStart(2, '0');
        const hours = String(currentDate.getHours()).padStart(2, '0');
        const minutes = String(currentDate.getMinutes()).padStart(2, '0');
        const seconds = String(currentDate.getSeconds()).padStart(2, '0');

        return `${rocYear}${month}${day}${hours}:${minutes}:${seconds}`;
    }

    function convertToROCYear(dateString) {
        if (!dateString) return '';

        dateString = dateString.replace(/\D/g, '');

        if (dateString.length !== 8) return '';

        const year = dateString.slice(0, 4);
        const month = dateString.slice(4, 6);
        const day = dateString.slice(6, 8);

        let rocYear = (parseInt(year) - 1911).toString();
        if (rocYear.length < 3) {
            rocYear = '0' + rocYear;
        }

        return `${rocYear}${month}${day}`;
    }

    function formatDate(dateString) {
        if (dateString.length === 7) {
            return `${dateString.substring(0, 3)}-${dateString.substring(3, 5)}-${dateString.substring(5, 7)}`;
        }
        return dateString;
    }

    function formatTime(timeString) {
        if (timeString.length === 4) {
            return `${timeString.substring(0, 2)} : ${timeString.substring(2, 4)}`;
        }
        return timeString;
    }

    function updateUserIdSelect(users) {
        const userIdSelect = $('#USERID');
        userIdSelect.empty().append('<option value="未選" selected>未選</option>');
        users.forEach(user => {
            const option = `<option value="${user.USERID}">${user.USERNAME}(${user.USERID})</option>`;
            userIdSelect.append(option);
        });
    }

    async function fetchNames(item) {
        const requestUrl = `${swaggerApiUrl}/api/MWu/GetNamesByIds?operuser=${item.OPERUSER}&cdoc=${item.CDOC}&cdocs=${item.CDOCS}&cdocp=${item.CDOCP}`;
        console.log('Fetching:', requestUrl);

        const res = await fetch(requestUrl);
        if (!res.ok) {
            throw new Error('Failed to fetch names');
        }

        const nameData = await res.json();
        return { ...item, ...nameData };
    }

    async function confirmDeletion(id) {
        if (confirm('確定要退掛嗎?')) {
            try {
                const updateData = {
                    WBID: id,
                    CSTAT: 'D',
                    CUPDATE: getCurrentROCYear()
                };

                const response = await fetch(`${swaggerApiUrl}/api/MWbe/PutWBEAURECstat/${id}`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(updateData)
                });
                if (response.ok) {
                    alert('退掛成功');
                    fetchAndUpdateCalendar();
                } else {
                    console.error('退掛失敗:', response.statusText);
                    alert('退掛失敗');
                }
            } catch (error) {
                console.error('退掛失敗:', error);
                alert('退掛失敗');
            }
        }
    }

    async function updateTable(data) {
        const tableBody = $('#registrationTable').DataTable();
        tableBody.clear().draw();

        const updatedData = await Promise.all(data.map(fetchNames));

        updatedData.forEach(item => {
            const acuteInfo = convertAcuteToValue(item.ACUTE);
            const rowNode = tableBody.row.add([
                acuteInfo.display,
                item.CNUM,
                item.CURENO,
                item.CURENAME,
                formatDate(item.RDATE),
                formatTime(item.RTIME),
                item.OPERUSERName,
                item.CDOCName,
                item.CDOCSName,
                item.CDOCPName,
                item.CURECNT,
                item.CURECNTS,
                `<a href="#" class="btn-delete" data-id="${item.WBID}" onclick="confirmDeletion(${item.WBID})">退掛</a>`
            ]).draw(false).node();
            $(rowNode).addClass(acuteInfo.className);
        });
    }
    async function fetchDataAndNames() {
        try {
            // Fetch initial data
            const response = await fetch(`${swaggerApiUrl}/api/MWbe/InitCalendar`);
            if (!response.ok) {
                throw new Error(`Network response was not ok: ${response.statusText}`);
            }
            const data = await response.json();
            console.log('Initial data:', data);

            const fetchPromises = data.map(fetchNames);
            const results = await Promise.all(fetchPromises);
            console.log('Fetch promises results:', results);
            updateTable(results);
        } catch (error) {
            console.error('Error fetching data:', error);
            $('#registrationTable').DataTable().clear().draw();
        }
    }

    async function fetchPageDataAndNames(page = 1, pageSize = 10) {
        try {
            const response = await fetch(`${swaggerApiUrl}/api/MWbe/InitCalendarPageData?page=${page}&pageSize=${pageSize}`);
            if (!response.ok) {
                throw new Error(`Network response was not ok: ${response.statusText}`);
            }
            const result = await response.json();
            console.log('Initial data:', result);

            const fetchPromises = result.Data.map(async item => {
                const requestUrl = `${swaggerApiUrl}/api/MWu/GetNamesByIds?operuser=${item.OPERUSER}&cdoc=${item.CDOC}&cdocs=${item.CDOCS}&cdocp=${item.CDOCP}`;
                console.log('Fetching:', requestUrl);

                const res = await fetch(requestUrl);
                if (!res.ok) {
                    throw new Error('Failed to fetch names');
                }
                const nameData = await res.json();
                return { ...item, ...nameData };
            });

            const results = await Promise.all(fetchPromises);
            console.log('Fetch promises results:', results);
            //updateTable(results);
            return {
                draw: result.draw || page,
                recordsTotal: result.TotalRecords,
                recordsFiltered: result.TotalRecords,
                data: results
            };
        } catch (error) {
            console.error('Error fetching data:', error);
            return {
                draw: page,
                recordsTotal: 0,
                recordsFiltered: 0,
                data: []
            };
        }
    }

    async function fetchAndUpdateCalendar() {
        const cnum = $('#CNUM').val().trim();
        const rdate = convertToROCYear($('#datetimepicker3').val().trim());
        const acute = convertAcuteToNumber($('#ACUTE').val());
        const cstat = $('#CSTAT').val();
        const userId = $('#USERID').val();

        const filterData = {
            cnum: cnum,
            rdate: rdate,
            acute: acute === "未選" ? "" : acute,
            cstat: cstat === "未選" ? "" : cstat,
            userId: userId === "未選" ? "" : userId
        };

        $.ajax({
            url: `${swaggerApiUrl}/api/MWbe/UpdateCalendar`,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(filterData),
            success: async function (data) {
                console.log('UpdateCalendar data:', data);
                const fetchPromises = data.map(fetchNames);
                const updatedData = await Promise.all(fetchPromises);
                updateTable(updatedData);
            },
            error: function (error) {
                console.error('Error fetching events:', error);
                $('#registrationTable').DataTable().clear().draw();
            }
        });
    }

    function clearBtn() {
        $('#CNUM').val('');
        $('#ACUTE, #CSTAT, #USERID').val('未選');
        datepicker3.clear();
        registrationTable.clear().draw();
    }

    $(document).ready(function () {
        $('#registrationTable').DataTable();
        fetchDataAndNames();

        $.get(`${swaggerApiUrl}/api/MWu`)
            .done(function (data) {
                updateUserIdSelect(data);
            })
            .fail(function (error) {
                console.error('Error fetching initial users:', error);
            });

        $('#ACUTE').change(fetchAndUpdateCalendar);
        $('#CSTAT').change(fetchAndUpdateCalendar);
        $('#USERID').change(fetchAndUpdateCalendar);

        let cnumTimeout;
        let rdateTimeout;

        $('#CNUM').on('input', function () {
            clearTimeout(cnumTimeout);
            cnumTimeout = setTimeout(() => {
                if ($(this).val().length === 10 || $(this).val().length === 0) {
                    fetchAndUpdateCalendar();
                }
            }, 500);
        });

        $('#datetimepicker3').on('input', function () {
            clearTimeout(rdateTimeout);
            rdateTimeout = setTimeout(() => {
                if ($(this).val().length >= 10 || $(this).val().length === 0) {
                    fetchAndUpdateCalendar();
                }
            }, 500);
        });

        $('#clearBtn').on('click', function () {
            clearTimeout(rdateTimeout);
            rdateTimeout = setTimeout(() => {
                clearBtn();
                fetchDataAndNames();
            }, 500);
        });

        $('#registrationTable tbody').on('click', '.btn-delete', function (event) {
            event.preventDefault();
            const id = $(this).data('id');
            confirmDeletion(id);
        });
    });
});
