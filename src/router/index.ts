import { createRouter, createWebHistory } from 'vue-router'
import AboutView from '@/views/AboutView.vue'
import TestView from '@/views/TestView.vue'
import StationView from '@/views/StationView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/about',
      name: 'about',
      component: AboutView
    },
    {
      path: '/test',
      name: 'test',
      component: TestView
    },
    {
      path: '/station',
      name: 'station',
      component: StationView
    }
  ]
})

export default router
