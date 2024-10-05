<script setup lang="ts">
import { getUserManager } from '@/services/oidc/UserManager';
import { useUserInfo } from '@/stores/current_user';

const userStore = useUserInfo();
getUserManager(userStore).signinSilentCallback();
</script>