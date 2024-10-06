<script setup lang="ts">
import { Button } from '@/components/ui/button'
import { UserCircle2Icon } from 'lucide-vue-next'
import { Skeleton } from '@/components/ui/skeleton';

import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from '@/components/ui/popover'

import { useUserInfo } from '@/stores/current_user';
import { getUserManager } from '@/services/oidc/UserManager';

const userStore = useUserInfo();

const mgr = getUserManager(userStore);

function signOut() {
  mgr.signoutRedirect()
}

//test only
async function refreshToken() {
  await mgr.signinSilent()
    .then(user => console.log('token renewed'))
    .catch(err => {
      alert('Session expired. Going out!');
      console.log(err);
      mgr.signoutRedirect()
        .then(resp => console.log('signed out', resp))
        .catch(err => console.log(err))
    })
}

</script>

<template>

  <Popover>
    <PopoverTrigger as-child>
      <Button class="flex items-center gap-3" variant="outline">
        <template v-if="userStore.user.isLoggedIn">
          <UserCircle2Icon />
          {{ userStore.user.info!.name ?? "Anonymous" }}
        </template>
        <template v-else>
          <Skeleton class="h-6 w-6 rounded-full" />
          <Skeleton class="h-4 w-[150px]" />
        </template>
      </Button>
    </PopoverTrigger>
    <PopoverContent>
      <template v-if="userStore.user.isLoggedIn">
        <Button variant="outline" :onclick="refreshToken">
          Refresh Token
        </Button>
        <Button variant="outline" :onclick="signOut">
          Sign Out
        </Button>
      </template>
      <template v-else>
        <div class="flex flex-col space-y-3">
          <Skeleton class="h-[125px] w-[250px] rounded-xl" />
          <div class="space-y-2">
            <Skeleton class="h-4 w-[250px]" />
            <Skeleton class="h-4 w-[200px]" />
          </div>
        </div>
      </template>
    </PopoverContent>
  </Popover>
</template>

<script lang="ts">
</script>