<script setup lang="ts">
import { useUserInfo } from '@/stores/current_user';
import UserWidget from './UserWidget.vue';
import Button from '../ui/button/Button.vue';
import { getUserManager } from '@/services/oidc/UserManager';

const userStore = useUserInfo();

function signIn() {
    getUserManager(userStore).signinRedirect();
}

</script>

<template>
    <UserWidget v-if="userStore.user.isLoggedIn"/>
    <template v-else>
        <Button variant="default" :onclick="signIn">
            Sign In
        </Button>
    </template>
</template>