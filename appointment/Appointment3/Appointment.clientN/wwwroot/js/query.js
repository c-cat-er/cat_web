document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('registrationForm');
    const swaggerApiUrl = form.getAttribute('data-swagger-api-url');
    const userId = form.getAttribute('data-user-id');
    const selectElement = $('#CURENO');
    const optionCountSpan = $('#cureNoOptionCount .count');
    let cureData = [];

    function initializeFlatpickr(selector, config) {
        return flatpickr(selector, config);
    }

    function initializeDataTable(selector, options = {}) {
        return $(selector).DataTable(Object.assign({
            paging: true,
            searching: false,
            lengthChange: false,
            pageLength: 7,
            responsive: true,
            language: {
                url: "https://cdn.datatables.net/plug-ins/2.0.8/i18n/zh-HANT.json"
            }
        }, options));
    }

    const datepickerConfig = {
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
                weekNumbers: true,
                longhand: ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月"],
            },
            firstDayOfWeek: 1,
        },
        time_24hr: true,
        scrollTitle: "滾動切換",
        confirmText: "OK",
    };

    const datepickerConfig2 = {
        ...datepickerConfig,
        minDate: "today",
        maxDate: new Date().fp_incr(90)
    };

    //const datepicker1 = initializeFlatpickr("#datetimepicker1", datepickerConfig1);
    const datepicker2 = initializeFlatpickr("#datetimepicker2", datepickerConfig2);

    const memberTable = initializeDataTable('#memberTable', {
        columnDefs: [
            { orderable: false, targets: '_all' },
            { className: 'dt-left', targets: '_all' }
        ],
        order: false
    });

    const cureTable = initializeDataTable('#cureTable', {
        language: {
            url: "https://cdn.datatables.net/plug-ins/2.0.8/i18n/zh-HANT.json",
            emptyTable: "沒有已購商品紀錄"
        },
        columnDefs: [
            { orderable: false, targets: [0, 2, 4, 5] },
            { className: 'select-checkbox', targets: 0 },
            { visible: false, targets: 1 },
            { className: 'dt-left', targets: [2, 3, 4, 5] },
            { className: 'dt-right', targets: [6, 7] },
            { width: '30%', targets: 5 }
        ],
        order: [[3, 'asc']]
    });

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
                return '0';
        }
    }

    function convertAcuteToValue(acute) {
        switch (acute) {
            case '1':
                return '看診';
            case '2':
                return '療程';
            case '3':
                return '諮詢';
            case '4':
                return '手術';
            default:
                return '無';
        }
    }

    function convertToROCYear(dateString) {
        if (!dateString) return '';

        dateString = dateString.replace(/[^0-9]/g, '');
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

    function convertToADYear(rocDateString) {
        if (!rocDateString || rocDateString.length !== 7) return '';
        const rocYear = rocDateString.slice(0, 3);
        const month = rocDateString.slice(3, 5);
        const day = rocDateString.slice(5, 7);
        const year = (parseInt(rocYear) + 1911).toString();
        return `${year}-${month}-${day}`;
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

    function debounce(func, wait) {
        let timeout;
        return function (...args) {
            const context = this;
            clearTimeout(timeout);
            timeout = setTimeout(() => func.apply(context, args), wait);
        };
    }

    function toggleAcuteSelect() {
        const acuteValue = $('#ACUTE').val();
        if (acuteValue === '療程') {
            $('#cureNoContainer').show();
        } else {
            $('#cureNoContainer').hide();
        }
    }

    function toggleCureNoSelect() {
        const cureNo = $('#CURENO').val();
        const acuteValue = $('#ACUTE').val();
        let selectedSerNo = "";
        let selectedSDate = "";
        const cure = cureData.find(item => item.CURENO === cureNo);
        if (acuteValue === '療程' && cure) {
            selectedSerNo = cure.SERNO;
            selectedSDate = cure.SDATE;
        }
    }

    function updateOptionCount() {
        const optionCount = selectElement.children('option').length;
        optionCountSpan.text(optionCount);
    }
    updateOptionCount();

    function updateDoctorDropdowns() {
        const acuteValue = convertAcuteToNumber($('#ACUTE').val());
        if (acuteValue == 0) {
            setDropdownOptions([], '#CDOC');
            setDropdownOptions([], '#CDOCS');
            setDropdownOptions([], '#CDOCP');
            return;
        }
        fetch(`${swaggerApiUrl}/api/MWu/GetUsersByAcute?acute=${acuteValue}`)
            .then(response => {
                if (!response.ok) { throw new Error(`HTTP error! status: ${response.status}`); }
                return response.json();
            })
            .then(data => {
                if (!Array.isArray(data)) { throw new Error('Unexpected data format'); }
                setDropdownOptions(data, '#CDOC');
                setDropdownOptions(data, '#CDOCS');
                setDropdownOptions(data, '#CDOCP');
            })
            .catch(error => console.error('Error fetching data:', error));
    }

    function setDropdownOptions(data, selector) {
        const dropdown = document.querySelector(selector);
        dropdown.innerHTML = '<option value="未選" selected>未選</option>';
        data.forEach(user => {
            const option = document.createElement('option');
            option.value = `${user.USERID}`;
            option.textContent = `${user.USERNAME}`;
            dropdown.appendChild(option);
        });
    }

    function removeDuplicateOptions() {
        const cdoc1 = $('#CDOC').val();
        const cdoc2 = $('#CDOCS').val();
        const cdoc3 = $('#CDOCP').val();

        const selectors = ['#CDOC', '#CDOCS', '#CDOCP'];

        selectors.forEach((selector, index) => {
            const otherValues = [cdoc1, cdoc2, cdoc3].filter((_, i) => i !== index);
            $(selector + ' option').each(function () {
                if ($(this).val() === '未選') {
                    $(this).prop('disabled', false);
                } else {
                    $(this).prop('disabled', otherValues.includes($(this).val()));
                }
            });
        });
    }

    function clearBtn() {
        $('#CNUM, #CNAME, #CBIRTH, #CMOBILE').val('');
        $('#ACUTE, #RTIME').val('未選');
        $('#ACUTE').val('未選');
        $('#CURENO').find('option').remove();
        $('#CMESS').val(null);
        updateOptionCount();
        //datepicker1.clear();
        datepicker2.clear();
        memberTable.clear().draw();
        cureTable.clear().draw();
        suppressInputEvent = false;
        setDropdownOptions([], '#CDOC');
        setDropdownOptions([], '#CDOCS');
        setDropdownOptions([], '#CDOCP');
    }

    function fetchCureData(cnum) {
        fetch(`${swaggerApiUrl}/api/MWc/QueryCustomerCure?cnum=${cnum}`)
            .then(response => {
                if (!response.ok) { throw new Error('no QueryCustomerCure data'); }
                return response.json();
            })
            .then(data => {
                cureData = data;
                cureTable.clear().draw();
                data.forEach(item => {
                    cureTable.row.add([
                        '',
                        item.SID,
                        item.SERNO,
                        item.SDATE,
                        item.CURENO,
                        item.CURENAME,
                        item.SUBCNT,
                        item.SUBQTY,
                    ]).draw();
                });
                suppressInputEvent = true;
            })
            .catch(error => {
                cureTable.clear().draw();
                //cureTable.row.add(['', '', '沒有商品紀錄', '', '', '', '', '']).draw();
                //$('#cureTableBody').append('<tr><td colspan="8" class="text-center">沒有商品紀錄</td></tr>');
                suppressInputEvent = true;
            })
            .finally(() => {
                $('#CURENO').find('option').remove();
                updateOptionCount();
            });
    }

    //輸入
    const handleInput = debounce(function () {
        if (suppressInputEvent) { return; }

        const cnum = document.getElementById('CNUM').value.trim();
        const cname = document.getElementById('CNAME').value.trim();
        const cbirth = document.getElementById('CBIRTH').value.trim()
        const cmobile = document.getElementById('CMOBILE').value.trim();

        if (cnum.length >= 7 || cname.length >= 1 || cbirth.length == 7 || cmobile.length >= 3) {
            fetch(`${swaggerApiUrl}/api/MWc/QueryCustomers`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    cnum: cnum,
                    cname: cname,
                    cbirth: cbirth,
                    cmobile: cmobile
                }),
                credentials: 'include'
            })
                .then(response => {
                    if (!response.ok) { throw new Error('no QueryCustomers data'); }
                    return response.json();
                })
                .then(data => {
                    memberTable.clear().draw();
                    if (data.length === 1) {
                        const item = data[0];

                        document.getElementById('CNUM').value = item.CNUM;
                        document.getElementById('CNAME').value = item.CNAME;
                        document.getElementById('CBIRTH').value = item.CBIRTH;
                        document.getElementById('CMOBILE').value = item.CMOBILE;

                        memberTable.row.add([
                            item.CNUM,
                            item.CNAME,
                            item.CBIRTH,
                            item.CMOBILE
                        ]).draw();

                        fetchCureData(item.CNUM);
                    } else {
                        data.forEach(item => {
                            memberTable.row.add([
                                item.CNUM,
                                item.CNAME,
                                item.CBIRTH,
                                item.CMOBILE
                            ]).draw();
                        });
                    }
                })
                .catch(error => {
                    console.error('QueryCustomers error fetching search results:', error);
                    memberTable.clear().draw();
                    memberTable.row.add([
                        '沒有會員資料', '', '', '',
                    ]).draw();
                });
        };
    }, 300);

    const inputs = document.querySelectorAll('#CNUM, #CNAME, #CBIRTH, #CMOBILE');
    let suppressInputEvent = true;

    inputs.forEach(input => {
        input.addEventListener('input', function () {
            suppressInputEvent = false;
            handleInput();
        });
    });

    $('#memberTable tbody').on('click', 'tr', function () {
        memberTable.$('tr.selected-row').removeClass('selected-row');
        $(this).addClass('selected-row');

        const rowMemberData = memberTable.row(this).data();
        if (rowMemberData) {
            const cnum = rowMemberData[0];
            const cname = rowMemberData[1];
            const cbirth = rowMemberData[2];
            const cmobile = rowMemberData[3];
            console.log(rowMemberData);

            document.getElementById('CNUM').value = cnum;
            document.getElementById('CNAME').value = cname;
            document.getElementById('CBIRTH').value = cbirth;
            document.getElementById('CMOBILE').value = cmobile;

            fetchCureData(cnum);
        }
    });

    $('#CDOC').change(function () {
        removeDuplicateOptions();
    });

    $('#CDOCS').change(function () {
        removeDuplicateOptions();
    });

    $('#CDOCP').change(function () {
        removeDuplicateOptions();
    });

    $('#ACUTE').change(function () {
        toggleAcuteSelect();
        updateDoctorDropdowns();
    });

    $('#CURENO').on('change', function () {
        toggleCureNoSelect();
    });

    $('#cureTable tbody').on('click', 'tr', function (event) {
        if (!$(event.target).is('input[type="checkbox"]')) {
            const row = $(this);
            const productExists = row.find('td').eq(2).text() !== "";
            if (productExists) {
                const isSelected = row.hasClass('selected');
                if (isSelected) {
                    cureTable.row(row).deselect();
                } else {
                    cureTable.row(row).select();
                }
                updateCureNoSelect(row);
            }
        }
    });

    function updateCureNoSelect(row) {
        const rowData = cureTable.row(row).data();
        console.log(rowData);
        $('#ACUTE').val('療程');
        toggleAcuteSelect();
        updateDoctorDropdowns();

        const cureNoElement = $('#CURENO');
        const currentOption = cureNoElement.find(`option[id="${rowData[1]}"]`);

        if (currentOption.length) {
            const previousOption = currentOption.prev();
            if (previousOption.length) {
                previousOption.prop('selected', true);
            }
            currentOption.remove();
        } else {
            cureNoElement.append('<option id="' + rowData[1] + '" value="' + rowData[4] + '" data-serno="' + rowData[2] + '" data-sdate="' + rowData[3] + '" selected>' + rowData[5] + '</option>');
        }
        updateOptionCount();
    }

    $('#clearBtn').on('click', function () {
        clearBtn();
    });

    function sanitizeString(str) {
        if (str != "未選") { return str.trim().replace(/[^a-zA-Z0-9]/g, ''); } else { return null; }
    }

    async function submitForm(event) {
        event.preventDefault();

        const form = document.getElementById('registrationForm');
        const formData = new FormData(form);

        const jsonObject = {};
        formData.forEach((value, key) => {
            jsonObject[key] = value;
        });

        const acuteValue = $('#ACUTE').val();
        const cureNoOptions = $('#CURENO').find('option');
        if (acuteValue === '療程') {
            const jsonObjects = [];
            const fetchPromises = [];

            cureNoOptions.each(function () {
                var option = $(this);
                var cureObject = { ...jsonObject };
                cureObject.SERNO = String(option.data('serno'));
                cureObject.SDATE = String(option.data('sdate'));
                cureObject.CURENO = String(option.val());
                cureObject.CURENAME = option.text();

                fetchPromises.push(
                    fetch(`${swaggerApiUrl}/api/MWu/GetCurecnt?CNUM=${cureObject.CNUM}&SERNO=${cureObject.SERNO}&CURENO=${cureObject.CURENO}`)
                        .then(response => {
                            if (!response.ok) {
                                throw new Error('Fetch error');
                            }
                            return response.json();
                        })
                        .then(data => {
                            cureObject.CURECNT = data[0].CURECNT;
                            cureObject.CURECNTS = data[0].CURECNTS;
                            jsonObjects.push(cureObject);
                        })
                        .catch(error => {
                            console.error('Error fetching CURECNT and CURECNTS:', error);
                        })
                );
            });

            try {
                await Promise.all(fetchPromises);

                console.log("jsonObject", jsonObject);
                jsonObjects.forEach(obj => {
                    obj.ACUTE = convertAcuteToNumber(obj.ACUTE);
                    obj.RDATE = convertToROCYear(obj.RDATE);
                    obj.RTIME = obj.RTIME.replace(':', '');
                    obj.OPERUSER = userId;
                    obj.CDOC = sanitizeString(obj.CDOC);
                    obj.CDOCS = sanitizeString(obj.CDOCS);
                    obj.CDOCP = sanitizeString(obj.CDOCP);
                    obj.CSTAT = 'A';
                    obj.CMESS = jsonObject.CMESS != "" ? jsonObject.CMESS : null;
                    obj.CUPDATE = getCurrentROCYear();
                    console.log('obj after processing:', obj);
                });
                console.log('提交前數據1:', jsonObject);

                if (jsonObject.RDATE != "" && jsonObject.RTIME != "未選" &&
                    jsonObject.CDOC != "未選" && jsonObject.CDOC != null) {
                    const finalResponse = await fetch(`${swaggerApiUrl}/api/MWbe/ListConfirmRegistrationBtn`, {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(jsonObjects)
                    });

                    if (!finalResponse.ok) {
                        const errorText = await finalResponse.text();
                        alert(errorText);
                    } else {
                        alert("預約成功");
                        clearBtn();
                    }
                } else {
                    alert("有欄位(預約類型/日期/時間/執行人員)，未選擇!");
                }
            } catch (error) {
                console.error('Error finalResponse:', error);
            }
        } else {
            jsonObject.SERNO = null;
            jsonObject.SDATE = null;
            jsonObject.CURENO = null;
            jsonObject.CURENAME = null;
            jsonObject.CURECNT = null;
            jsonObject.CURECNTS = null;

            jsonObject.ACUTE = convertAcuteToNumber(jsonObject.ACUTE);
            jsonObject.CNUM = jsonObject.CNUM;
            jsonObject.RDATE = convertToROCYear(jsonObject.RDATE);
            jsonObject.RTIME = jsonObject.RTIME.replace(':', '');
            jsonObject.OPERUSER = userId;
            jsonObject.CDOC = sanitizeString(jsonObject.CDOC);
            jsonObject.CDOCS = sanitizeString(jsonObject.CDOCS);
            jsonObject.CDOCP = sanitizeString(jsonObject.CDOCP);
            jsonObject.CSTAT = 'A';
            jsonObject.CMESS = jsonObject.CMESS != "" ? jsonObject.CMESS : null;
            jsonObject.CUPDATE = getCurrentROCYear();
            console.log('提交前數據2:', jsonObject);

            if (jsonObject.ACUTE != "0" && jsonObject.RDATE != "" &&
                jsonObject.RTIME != "未選" && jsonObject.CDOC != null) {
                try {
                    const finalResponse = await fetch(`${swaggerApiUrl}/api/MWbe/ConfirmRegistrationBtn`, {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(jsonObject)
                    });

                    if (!finalResponse.ok) {
                        const errorText = await finalResponse.text();
                        alert(errorText);
                    }

                    const result = await finalResponse.json();
                    alert("預約成功");
                    clearBtn();
                } catch (error) {
                    console.error('submit falut:', error);
                }
            } else {
                alert("有欄位(預約類型/日期/時間/執行人員)，未選擇!");
            }
        }
    }

    $('#confirmRegistrationBtn').on('click', function (event) {
        submitForm(event);
    });
});
