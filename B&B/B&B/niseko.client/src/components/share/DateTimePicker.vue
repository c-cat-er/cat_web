<!--@/components/share/DateTimePicker.vue-->
<!--use Vuetify Date and Time Picker-->
<template>
    <v-menu v-model="internalPickerModel"
            :close-on-content-click="false"
            transition="scale-transition"
            offset-y
            min-width="290px">
        <template v-slot:activator="{ on, attrs }">
            <v-text-field :value="internalValue"
                          @input="updateModelValue"
                          :label="label"
                          readonly
                          v-bind="attrs"
                          v-on:click="on?.click"
                          v-on:focus="on?.focus"
                          v-on:blur="on?.blur"></v-text-field>
        </template>
        <v-date-picker v-model="internalValue"
                       @change="updateModelValue"
                       color="primary"
                       type="datetime"></v-date-picker>
    </v-menu>
</template>

<script setup>
    import { ref, watch, defineEmits } from 'vue';

    const props = defineProps({
        modelValue: {
            type: String,
            required: true,
        },
        label: {
            type: String,
            default: '',
        },
        pickerModel: {
            type: Boolean,
            required: true,
        },
        onChange: {
            type: Function,
            required: true,
        },
    });

    const emit = defineEmits(['update:modelValue', 'update:pickerModel']);
    const internalValue = ref(props.modelValue || '');
    const internalPickerModel = ref(props.pickerModel || false);

    // 監聽 modelValue 和 pickerModel 的變化
    watch(
        () => props.modelValue,
        (newValue) => {
            internalValue.value = newValue || '';
        }
    );

    watch(
        () => props.pickerModel,
        (newValue) => {
            internalPickerModel.value = newValue;
        }
    );

    // 更新 internalValue 並觸發事件通知父組件
    const updateModelValue = (value) => {
        try {
            internalValue.value = value;
            emit('update:modelValue', value);
            if (props.onChange) props.onChange(value);
        } catch (error) {
            console.error("Error updating model value:", error);
        }
    };

    // 更新 pickerModel 的狀態並通知父組件
    const handlePickerModelChange = (value) => {
        try {
            internalPickerModel.value = value;
            emit('update:pickerModel', value);
        } catch (error) {
            console.error("Error updating picker model:", error);
        }
    };
</script>

<!--modelValue: 這個 prop 用來接收綁定的日期時間數據，並與 v-model 進行雙向綁定。
label: 用來設置 v-text-field 的標籤。
pickerModel: 用來控制 v-menu 的顯示或隱藏。
onChange: 傳遞一個方法，當日期時間改變時觸發。-->
