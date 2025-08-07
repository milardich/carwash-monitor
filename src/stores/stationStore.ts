import { defineStore } from "pinia";
import type { Station } from "@/api/station.api";


export const useStationStore = defineStore('station', {
    state: () => ({
        selectedStation: null as Station | null,
        stations: [] as Station[]
    }),
    actions: {},
});