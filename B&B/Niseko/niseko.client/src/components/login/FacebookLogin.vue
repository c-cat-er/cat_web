<!--@/components/login/FacebookLogin.vue -->
<template>

</template>

<style scoped>
</style>

<script setup>
    import axios from 'axios';

    export default {
        name: 'ExampleComponent',
        data() {
            return {
                response: null,
            };
        },
        mounted() {
            axios.get('https://api.example.com/data')
                .then(response => {
                    this.response = response.data;
                })
                .catch(error => {
                    console.error(error);
                });
        }
    };
</script>
