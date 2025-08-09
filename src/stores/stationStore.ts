import { defineStore } from "pinia";
import type { Station } from "@/api/station.api";
import { getAllStations } from "@/api/station.api";

export const useStationStore = defineStore('station', {
    state: () => ({
        selectedStation: null as Station | null,
        stations: [] as Station[],
        isLoading: false,
    }),
    
    actions: {
        async loadStations() {
            this.isLoading = true;
            try {
                this.stations = await getAllStations();
                if (!this.selectedStation && this.stations.length) {
                    this.selectedStation = this.stations[0];
                }
            } finally {
                this.isLoading = false;
            }
        },
        selectStation(station: Station) {
            this.selectedStation = station;
        }
    },

    getters: {
        hasStations: (state) => state.stations.length > 0,
    }
});