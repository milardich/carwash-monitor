// import { reactive } from 'vue';
// import { type Station } from '@/api/station.api'

// export const stationStore = reactive({
//     stations: [] as Station[],
//     selectedStation: null as Station | null
// });


import { defineStore } from "pinia";
import type { Station } from "@/api/station.api";


export const useStationStore = defineStore('station', {
    state: () => ({
        selectedStation: undefined as Station | undefined,
        stations: [] as Station[]
    }),
    actions: {},
});