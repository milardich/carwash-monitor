import { defineStore } from 'pinia'
import type { Station } from '@/api/station.api'
import { getAllStations, getStation } from '@/api/station.api'
import { useResourceStore } from './resourceStore'
import { useBoxStore } from './boxStore'

// stationStore.ts
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
          await this.switchStation(this.stations[0].id)
        }
      } finally {
        this.isLoading = false
      }
    },

    async switchStation(stationId: string) {
      const station = await getStation(stationId)
      if (station) {
        this.selectedStation = station

        // Load all related data when station changes
        const resourceStore = useResourceStore()
        const boxStore = useBoxStore()

        await Promise.all([
          resourceStore.loadResourceConsumptions(station.id),
          boxStore.loadBoxes(station.id) // you'd need to implement this
        ])
      }
    }
  },

  getters: {
    hasStations: (state) => state.stations.length > 0
  }
})
