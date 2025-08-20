import type { StatisticsHighlights, StatisticsSummary } from '@/api/statistics.api'
import { defineStore } from 'pinia'
import { getStatisticsHighlights } from '@/api/statistics.api'
import { getStatisticsSummary } from '@/api/statistics.api'

export const useStatisticsStore = defineStore('statistics', {
    state: () => ({
        statisticsHighlights: null as StatisticsHighlights | null,
        statisticsSummary: null as StatisticsSummary | null,
        isLoading: false
    }),

    actions: {
        async loadStatistics() {
            this.isLoading = true
            try {
                const [highlights, summary] = await Promise.all([
                    getStatisticsHighlights(),
                    getStatisticsSummary()
                ])
                this.statisticsHighlights = highlights
                this.statisticsSummary = summary
            } finally {
                this.isLoading = false
            }
        }
    },

    getters: {}
})
