import { defineStore } from 'pinia'
import type { Station } from '@/api/station.api'
import { getAllStations } from '@/api/station.api'
import { useResourceStore } from './resourceStore'

export const useStationStore = defineStore('station', {
    state: () => ({
        selectedStation: null as Station | null,
        stations: [] as Station[],
        isLoading: false
    }),

    actions: {
        async loadStations() {
            this.isLoading = true
            try {
                this.stations = await getAllStations()
                if (!this.selectedStation && this.stations.length) {
                    this.selectedStation = this.stations[0]
                }
            } finally {
                this.isLoading = false
            }
        },

        async switchStation(station: Station) {
            if (station) {
                console.log('Switched to station: ', station)
                this.selectedStation = station
                const resourceStore = useResourceStore()
                await resourceStore.loadResourceConsumptions(station.id)
            }
        }
    },

    getters: {
        hasStations: (state) => state.stations.length > 0
    }
})
