import { defineStore } from 'pinia'
import type { Station } from '@/api/station.api'
import { getAllStations, getStation, createStation } from '@/api/station.api'
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
        },

        async createStation(name: string) {
            if (!name) return
            this.isLoading = true
            try {
                await createStation({ name })
                await this.loadStations()
            } finally {
                this.isLoading = false
            }
        }
    },

    getters: {
        hasStations: (state) => state.stations.length > 0
    }
})
