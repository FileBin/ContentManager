import { ref } from 'vue'
import { defineStore } from 'pinia'
import { UserInfo } from '@/lib/user-info'
import { getUserManager } from '@/services/oidc/UserManager'

export class CurrentUser {
  get isLoggedIn(): boolean {
    return this.info !== null
  }

  info: UserInfo | null = null
}

export const useUserInfo = defineStore('current_user', () => {
  const user = ref(new CurrentUser())

  getUserManager().getUser().then(oidc_user => {
    if (oidc_user !== null) {
      user.value.info = UserInfo.fromProfile(oidc_user.profile)
    }
  })


  return { user }
})