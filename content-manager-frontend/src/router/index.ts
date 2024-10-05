import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/posts/recent',
      name: 'recent-posts',
      component: () => import('../views/RecentPosts.vue')
    },
    {
      path: '/oidc/callback',
      name: 'oidc-callback',
      component: () => import('../views/oidc/Callback.vue')
    },
    {
      path: '/oidc/silent-renew',
      name: 'oidc-silent-renew',
      component: () => import('../views/oidc/SilentRenew.vue')
    }
  ]
})

export default router
