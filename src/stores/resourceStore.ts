import type { ChartResponse } from '@/api/resources.api'
import { getChartData } from '@/api/resources.api'
import { defineStore } from 'pinia'

export const useResourceStore = defineStore('resources', {
    state: () => ({
        waterData: [] as number[],
        waxData: [] as number[],
        detergentData: [] as number[],
        labels: [] as string[],
        isLoading: false
    }),

    actions: {
        async loadResourceConsumptions(stationId: string) {
            this.isLoading = true
            try {
                const data: ChartResponse = await getChartData(stationId)
                this.labels = data.water.labels
                this.waterData = data.water.data
                this.waxData = data.wax.data
                this.detergentData = data.detergent.data
            } finally {
                this.isLoading = false
            }
        }
    },

    getters: {}
})
