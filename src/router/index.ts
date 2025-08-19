import { createRouter, createWebHistory } from 'vue-router'
import DashboardView from '@/views/DashboardView.vue'
import ControlPanelView from '@/views/ControlPanelView.vue'
import StatisticsView from '@/views/StatisticsView.vue'

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/',
            redirect: {
                path: '/dashboard'
            }
        },
        {
            path: '/dashboard',
            name: 'dashboard',
            component: DashboardView
        },
        {
            path: '/statistics',
            name: 'statistics',
            component: StatisticsView
        },
        {
            path: '/control-panel',
            name: 'control-panel',
            component: ControlPanelView
        }
    ]
})

export default router
