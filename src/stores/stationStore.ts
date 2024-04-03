import { reactive } from 'vue';
import { type Station } from '@/api/station.api'

export const stationStore = reactive({
    stations: [] as Station[],
    selectedStation: null as Station | null
});
