document.addEventListener('DOMContentLoaded', function () {
    const swaggerApiUrl = document.getElementById('calendarForm').dataset.swaggerApiUrl;

    function initializeFlatpickr(selector, config) {
        return flatpickr(selector, config);
    }

    const datepickerConfig4 = {
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

    const datepicker4 = initializeFlatpickr("#datetimepicker4", datepickerConfig4);

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

    function updateUserIdSelect(users) {
        const userIdSelect = $('#USERID');
        userIdSelect.empty().append('<option value="未選" selected>未選</option>');
        users.forEach(user => {
            const option = `<option value="${user.USERID}">${user.USERNAME}(${user.USERID})</option>`;
            userIdSelect.append(option);
        });
    }

    function clearBtn() {
        $('#CNUM').val('');
        $('#ACUTE, #CSTAT, #USERID').val('未選');
        datepicker4.clear();
    }

    $(document).ready(function () {
        var calendarEl = document.getElementById('calendar');

        var calendar = new FullCalendar.Calendar(calendarEl, {
            headerToolbar: {
                left: 'prev,next today',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek,timeGridDay'
            },
            initialView: 'dayGridMonth',
            editable: true,
            droppable: true,
            nowIndicator: true,
            firstDay: 1,
            locale: 'zh-tw',
            views: {
                dayGridMonth: { buttonText: '月' },
                timeGridWeek: { buttonText: '週' },
                timeGridDay: { buttonText: '日' }
            },
            navLinks: true,
            navLinkWeekClick: function (weekStart, jsEvent) {
                calendar.changeView('timeGridWeek', weekStart);
            },
            navLinkDayClick: function (date, jsEvent) {
                if (calendar.view.type === 'dayGridMonth') {
                    calendar.changeView('timeGridWeek', date);
                } else if (calendar.view.type === 'timeGridWeek') {
                    calendar.changeView('timeGridDay', date);
                }
            },
            eventContent: function (arg) {
                let tooltip = $('<div class="fc-event-tooltip">' + arg.event.title + '</div>');
                $(arg.el).append(tooltip);
                return { html: '<div class="fc-event-title">' + arg.event.title + '</div>' };
            },
            eventClassNames: function (arg) {
                switch (arg.event.extendedProps.type) {
                    case '諮詢':
                        return ['event-consultation'];
                    case '看診':
                        return ['event-medical'];
                    case '手術':
                        return ['event-surgery'];
                    case '療程':
                        return ['event-therapy'];
                }
            },
            eventClick: function (info) {
                console.log("eventClick");
            },
            events: [],
        });
        calendar.render();
        
        function fetchNames(operuser, cdoc, cdocs, cdocp) {
            return $.ajax({
                url: `${swaggerApiUrl}/api/MWu/GetNamesByIds`,
                type: 'GET',
                data: { operuser: operuser, cdoc: cdoc, cdocs: cdocs, cdocp: cdocp },
                success: function (data) {
                    let CDOCSName = data.CDOCSName === null ? "" : data.CDOCSName;
                    let CDOCPName = data.CDOCPName === null ? "" : data.CDOCPName;
                    return {
                        OPERUSERName: data.OPERUSERName,
                        CDOCName: data.CDOCName,
                        CDOCSName: CDOCSName,
                        CDOCPName: CDOCPName
                    };
                },
                error: function (error) {
                    console.error('Error fetching names:', error);
                    return {
                        OPERUSERName: '',
                        CDOCName: '',
                        CDOCSName: '',
                        CDOCPName: ''
                    };
                }
            });
        }

        function fetchAndRenderEvents(apiUrl, filterData = {}) {
            $.ajax({
                url: apiUrl,
                type: 'GET',
                contentType: 'application/json',
                success: function (data) {
                    var events = data.map(event => {
                        let acuteValue = convertAcuteToValue(event.ACUTE);

                        const rdate = event.RDATE;
                        const rtime = event.RTIME;

                        const year = (parseInt(rdate.substring(0, 3)) + 1911).toString();
                        const month = rdate.substring(3, 5);
                        const day = rdate.substring(5, 7);
                        const hours = rtime.substring(0, 2);
                        const minutes = rtime.substring(2, 4);

                        var start = moment(`${year}-${month}-${day} ${hours}:${minutes}`, "YYYY-MM-DD HH:mm").toISOString();
                        var end = moment(start).add(30, 'minutes').toISOString();

                        return fetchNames(event.OPERUSER, event.CDOC, event.CDOCS, event.CDOCP).then(names => {
                            let title = `${hours}:${minutes}${acuteValue}${event.CURENAME ? `/${event.CURENAME}` : ""}(${names.OPERUSERName}/${names.CDOCName}`;
                            if (names.CDOCSName) {
                                title += `,${names.CDOCSName}`;
                            }
                            if (names.CDOCPName) {
                                title += `,${names.CDOCPName}`;
                            }
                            title += `)`;

                            return {
                                title: title,
                                start: start,
                                end: end,
                                extendedProps: {
                                    type: acuteValue
                                }
                            };
                        });
                    });
                    Promise.all(events).then(resolvedEvents => {
                        calendar.removeAllEvents();
                        calendar.addEventSource(resolvedEvents);
                    });
                },
                error: function (error) {
                    console.error('Error fetching events:', error);
                }
            });
        }

        fetchAndRenderEvents(`${swaggerApiUrl}/api/MWbe/InitCalendar`);

        $.get(`${swaggerApiUrl}/api/MWu`)
            .done(function (data) {
                updateUserIdSelect(data);
            })
            .fail(function (error) {
                console.error('Error fetching initial users:', error);
            });

        //$('#ACUTE').change(function () {
        //    const acuteNumber = convertAcuteToNumber($(this).val());
        //    $.get(`${swaggerApiUrl}/api/MWu/GetUsersByAcute`, { acute: acuteNumber })
        //        .done(function (data) {
        //            updateUserIdSelect(data);
        //        })
        //        .fail(function (error) {
        //            console.error('Error fetching users by acute value:', error);
        //        });
        //});

        function fetchAndUpdateCalendar() {
            const cnum = $('#CNUM').val().trim();
            const rdate = convertToROCYear($('#datetimepicker4').val().trim());
            const acute = convertAcuteToNumber($('#ACUTE').val());
            const cstat = $('#CSTAT').val();
            const userId = $('#USERID').val();

            var filterData = {
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
                success: function (data) {
                    var events = data.map(event => {
                        let acuteValue = convertAcuteToValue(event.ACUTE);

                        const rdate = event.RDATE;
                        const rtime = event.RTIME;

                        const year = (parseInt(rdate.substring(0, 3)) + 1911).toString();
                        const month = rdate.substring(3, 5);
                        const day = rdate.substring(5, 7);
                        const hours = rtime.substring(0, 2);
                        const minutes = rtime.substring(2, 4);

                        var start = moment(`${year}-${month}-${day} ${hours}:${minutes}`, "YYYY-MM-DD HH:mm").toISOString();
                        var end = moment(start).add(30, 'minutes').toISOString();

                        return fetchNames(event.OPERUSER, event.CDOC, event.CDOCS, event.CDOCP).then(names => {
                            let title = `${hours}:${minutes}${acuteValue}${event.CURENAME ? `/${event.CURENAME}` : ""}(${names.OPERUSERName}/${names.CDOCName}`;
                            if (names.CDOCSName) {
                                title += `,${names.CDOCSName}`;
                            }
                            if (names.CDOCPName) {
                                title += `,${names.CDOCPName}`;
                            }
                            title += `)`;

                            return {
                                title: title,
                                start: start,
                                end: end,
                                extendedProps: {
                                    type: acuteValue
                                }
                            };
                        });
                    });

                    Promise.all(events).then(resolvedEvents => {
                        calendar.removeAllEvents();
                        calendar.addEventSource(resolvedEvents);
                    });
                },
                error: function (error) {
                    console.error('Error fetching events:', error);
                    calendar.removeAllEvents();
                }
            });
        }

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

        $('#datetimepicker4').on('input', function () {
            clearTimeout(rdateTimeout);
            rdateTimeout = setTimeout(() => {
                if ($(this).val().length >= 10 || $(this).val().length === 0) {
                    fetchAndUpdateCalendar();
                }
            }, 500);
        });

        $('#ACUTE, #CSTAT, #USERID').change(fetchAndUpdateCalendar);

        $('#clearBtn').on('click', function () {
            clearTimeout(rdateTimeout);
            rdateTimeout = setTimeout(() => {
                clearBtn();
                fetchAndUpdateCalendar();
            }, 500);
        });
    });
});
