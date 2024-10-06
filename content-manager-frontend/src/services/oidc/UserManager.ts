import Oidc from 'oidc-client';

import { UserInfo } from '@/lib/user-info';

let mgr_instance: Oidc.UserManager | null = null;
let events_registered = false;

export function getUserManager(userStore?: any): Oidc.UserManager {
  Oidc.Log.logger = console;
  Oidc.Log.level = Oidc.Log.INFO;

  if (mgr_instance == null) {
    mgr_instance = new Oidc.UserManager({
      userStore: new Oidc.WebStorageStateStore({}),
      authority: 'http://keycloak.content-manager.local/realms/ContentManager',
      client_id: 'content_manager_browser',
      redirect_uri: window.location.origin + '/oidc/callback',
      response_type: 'id_token token',
      scope: 'openid offline_access',
      post_logout_redirect_uri: window.location.origin + '/',
      silent_redirect_uri: window.location.origin + '/oidc/silent-renew',
      includeIdTokenInSilentRenew: true,
      accessTokenExpiringNotificationTime: 360,
      automaticSilentRenew: false,
      filterProtocolClaims: true,
      loadUserInfo: true,
    })
  }
  if (events_registered) {
    return mgr_instance;
  }

  if (userStore && !events_registered) {
    events_registered = true;
    mgr_instance.events.addUserLoaded(user => {
      console.log('User Loaded');
      userStore.user.info = UserInfo.fromProfile(user.profile);

    });

    mgr_instance.events.addUserUnloaded(() => {
      userStore.user.info = null;
    })

    mgr_instance.events.addAccessTokenExpiring(function () {
      console.log('Refreshing AccessToken:', arguments);
      mgr_instance?.signinSilent();
    });

    mgr_instance.events.addAccessTokenExpired(function () {
      console.log('AccessToken Expired:', arguments);
      mgr_instance?.signinSilent()
        .then(user => console.log('token renewed'))
        .catch(err => {
          alert('Session expired. Going out!');
          console.log(err);
          mgr_instance?.signoutRedirect()
            .then(resp => console.log('signed out', resp))
            .catch(err => console.log(err))
        })
    });

    mgr_instance.events.addUserSignedOut(function () {
      alert('Going out!');
      console.log('UserSignedOut:', arguments);
      mgr_instance?.signoutRedirect()
        .then(resp => console.log('signed out', resp))
        .catch(err => console.log(err))
    });
  }

  return mgr_instance;
}