<!-- @/components/share/Flatpickr.vue -->
<template>
    <div>
        <!--起始-->
        <v-text-field v-model="displayValue1"
                      :label="labelStart"
                      readonly
                      @click="togglePicker">
        </v-text-field>
        <!--結束-->
        <v-text-field v-model="displayValue2"
                      :label="labelEnd"
                      readonly
                      @click="togglePicker">
        </v-text-field>
        <!-- 用于 flatpickr 的挂载点 -->
        <div ref="datepicker"></div>
        <!-- 隐藏的第二个输入框，由 rangePlugin 使用 -->
        <input type="text" id="secondRangeInput" style="display:none;" />
    </div>
</template>
<script>
    import { ref, onMounted, onBeforeUnmount, nextTick } from 'vue';
    import flatpickr from 'flatpickr';
    import 'flatpickr/dist/flatpickr.min.css';
    import { MandarinTraditional } from 'flatpickr/dist/l10n/zh-tw.js';
    import rangePlugin from 'flatpickr/dist/plugins/rangePlugin';

    export default {
        props: {
            modelValue1: String,
            modelValue2: String,
            labelStart: {
                type: String,
                default: '選擇開始日期',
            },
            labelEnd: {
                type: String,
                default: '選擇結束日期',
            },
            disableDates: {
                type: Array,
                default: () => [],
            },
        },
        emits: ['update:modelValue1', 'update:modelValue2'],
        setup(props, { emit }) {
            const datepicker = ref(null);
            const displayValue1 = ref(props.modelValue1 || '');
            const displayValue2 = ref(props.modelValue2 || '');
            let fp = null;

            const updateDisplayValue = (dates) => {
                /* 09/05 update
                if (dates && dates.length === 2) {
                    const [startDate, endDate] = dates;
                    //use 本地時間
                    const formatDate = (date) => date ? date.toLocaleString('zh-TW', { hour12: false }) : '';

                    displayValue1.value = formatDate(startDate);
                    displayValue2.value = formatDate(endDate);
                    //console.log('Emitting update:modelValue1:', startDate);
                    //console.log('Emitting update:modelValue2:', endDate);
                } */
                const formatDate = (date) => date ? date.toLocaleString('zh-TW', { hour12: false }) : '';
                displayValue1.value = formatDate(dates[0]);
                displayValue2.value = formatDate(dates[1]);
            };

            const togglePicker = () => {
                if (fp) {
                    fp.open();
                }
            };

            //清除日期功能
            const clearDates = () => {
                if (fp) {
                    fp.clear(); //清除 Flatpickr 的值
                    displayValue1.value = ''; //清除显示的开始日期
                    displayValue2.value = ''; //清除显示的结束日期
                    emit('update:modelValue1', ''); //发射空字符串，清除绑定的值
                    emit('update:modelValue2', '');
                }
            };

            onMounted(() => {
                nextTick(() => {
                    if (datepicker.value) {
                        console.log("Initializing Flatpickr...");
                        fp = flatpickr(datepicker.value, {
                            locale: MandarinTraditional,
                            allowInput: false,
                            enableTime: true,
                            dateFormat: "Y-m-d H:i",
                            time_24hr: true,
                            minDate: "today",
                            disable: [
                                ...props.disableDates,
                                (date) => props.disableDates.includes(date.toISOString().slice(0, 10))
                            ],
                            defaultDate: [props.modelValue1, props.modelValue2],
                            onValueUpdate: (selectedDates) => {
                                /* 09/05 remove
                                if (selectedDates.length === 1) {
                                    selectedDates[1] = selectedDates[0];
                                }
                                // Sync start time with end time
                                selectedDates[0].setHours(selectedDates[1].getHours());
                                selectedDates[0].setMinutes(selectedDates[1].getMinutes()); */

                                updateDisplayValue(selectedDates);

                                //console.log("Selected dates:", selectedDates);
                                //k-p, 此處接收各組件的日期時間後，使用 UTC 時間，各 store 內再轉換為本地時間(db)。
                                //因若此處改用本地時間，則日期時間單位數會沒有0，如此各 store 則還需額外邏輯判斷，複雜。
                                //emit('update:modelValue1', startDate ? formatDate(startDate) : '');
                                //emit('update:modelValue2', endDate ? formatDate(endDate) : '');
                                emit('update:modelValue1', selectedDates[0] ? selectedDates[0].toISOString().slice(0, 19).replace('T', ' ') : '');
                                emit('update:modelValue2', selectedDates[1] ? selectedDates[1].toISOString().slice(0, 19).replace('T', ' ') : '');
                            },
                            plugins: [
                                rangePlugin({ input: "#secondRangeInput" })
                            ],
                        });

                        updateDisplayValue(fp.selectedDates);
                    } else {
                        console.error('datepicker.value is null or undefined');
                    }
                });
            });

            onBeforeUnmount(() => {
                if (fp) {
                    fp.destroy();
                }
            });

            return { //將方法暴露給外部使用
                datepicker,
                displayValue1,
                displayValue2,
                togglePicker,
                clearDates,
            };
        }
    };
</script>
<style scoped>
    @import "/node_modules/flatpickr/dist/flatpickr.min.css";
    .flatpickr-input {
        display: none;
    }
</style>
<!--為了讓 Flatpickr.vue 能夠在不同的情境下被使用，同時允許某些組件在選擇起始日期後自動加一天，而其他組件可能不需要此功能，我們可以通過添加一個可選的 prop 來控制這一行為。
    這樣，在需要自動加一天的組件中，可以傳遞該 prop，而在不需要的組件中則不傳遞或設置為 false。-->
