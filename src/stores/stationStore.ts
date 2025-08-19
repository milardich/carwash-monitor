import { defineStore } from 'pinia'
import type { Station } from '@/api/station.api'
import { getAllStations, getStation } from '@/api/station.api'
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

        async updateSelectedStation() {
            this.isLoading = true
            try {
                if (this.selectedStation != null)
                    this.selectedStation = await getStation(this.selectedStation.id)
            } finally {
                this.isLoading = false
            }
        },

        async switchStation(station: Station) {
            if (station) {
                console.log('Switched to station: ', station)
                this.selectedStation = station
                await this.updateSelectedStation()
                const resourceStore = useResourceStore()
                await resourceStore.loadResourceConsumptions(station.id)
            }
        }
    },

    getters: {
        hasStations: (state) => state.stations.length > 0
    }
})
