<!--@/components/login/GoogleLogin.vue -->
<template>
    <div style="display: flex; justify-content: center;">
        <div id="google-sign-in-button"></div>
    </div>
</template>

<script>
    export default {
        name: 'GoogleSignInButton',
        props: {
            width: {
                type: String,
                default: null
            },
            height: {
                type: String,
                default: null
            }
        },
        mounted() {
            this.loadGoogleScript();
        },
        methods: {
            loadGoogleScript() {
                const script = document.createElement('script');
                script.src = "https://accounts.google.com/gsi/client";
                script.async = true;
                script.onload = () => this.initializeGoogleSignIn();
                document.head.appendChild(script);
            },
            initializeGoogleSignIn() {
                window.google.accounts.id.initialize({
                    client_id: "您的客户端ID",
                    callback: this.handleCredentialResponse
                });
                window.google.accounts.id.renderButton(
                    document.getElementById("google-sign-in-button"),
                    {
                        theme: "outline",
                        size: "large",
                        shape: "pill",
                    }
                );
            },
            parseJwt(token) {
                var base64Url = token.split('.')[1];
                var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
                var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
                    return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
                }).join(''));
                return JSON.parse(jsonPayload);
            },
            handleCredentialResponse(response) {
                const data = this.parseJwt(response.credential);
                console.log(data);
            },
        }
    };
</script>
