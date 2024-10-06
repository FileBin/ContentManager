import Oidc from 'oidc-client';

import { UserInfo } from '@/lib/user-info';

export function getUserManager(userStore?: any): Oidc.UserManager {
  const mgr = new Oidc.UserManager({
    userStore: new Oidc.WebStorageStateStore({}),
    authority: 'http://keycloak.content-manager.local/realms/ContentManager',
    client_id: 'content_manager_browser',
    redirect_uri: window.location.origin + '/oidc/callback',
    response_type: 'id_token token',
    scope: 'openid offline_access',
    post_logout_redirect_uri: window.location.origin + '/',
    silent_redirect_uri: window.location.origin + '/oidc/silent-renew',
    accessTokenExpiringNotificationTime: 10,
    automaticSilentRenew: true,
    filterProtocolClaims: true,
    loadUserInfo: true
  })

  Oidc.Log.logger = console;
  Oidc.Log.level = Oidc.Log.INFO;
  mgr.events.addUserLoaded(user => {
    console.log('User Loaded');
    if (userStore) {
      userStore.user.info = UserInfo.fromProfile(user.profile);
    }
  });

  mgr.events.addUserUnloaded(() => {
    if (userStore) {
      userStore.user.info = null;
    }
  })

  mgr.events.addAccessTokenExpiring(function () {
    console.log('AccessToken Expiring:', arguments);
  });

  mgr.events.addAccessTokenExpired(function () {
    console.log('AccessToken Expired:', arguments);
    mgr.signinSilent()
    .then(user => console.log('token renewed'))
    .catch(err => {
      alert('Session expired. Going out!');
      mgr.signoutRedirect()
      .then(resp => console.log('signed out', resp))
      .catch(err => console.log(err))
    })
  });

  mgr.events.addSilentRenewError(function () {
    console.error('Silent Renew Error:', arguments);
  });

  mgr.events.addUserSignedOut(function () {
    alert('Going out!');
    console.log('UserSignedOut:', arguments);
    mgr.signoutRedirect()
      .then(resp => console.log('signed out', resp))
      .catch(err => console.log(err))
  });

  return mgr;
}