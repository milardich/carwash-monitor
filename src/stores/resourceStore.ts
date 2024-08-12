import type { ResourceConsumption } from '@/api/resources.api';
import { reactive } from 'vue';
import { getChartData } from '@/api/resources.api';
import { stationStore } from './stationStore';
import { computed } from 'vue';

export const resourceStore = reactive({
    resourceConsumptions: [] as ResourceConsumption[],
    pgTimeInterval: "" as String,
});

export async function setChartDataByStationId(stationId: number) {
    var newData: ResourceConsumption[] = await getChartData(stationId, resourceStore.pgTimeInterval);
    if(newData[newData.length - 1] != resourceStore.resourceConsumptions[resourceStore.resourceConsumptions.length - 1]) {
        resourceStore.resourceConsumptions = newData;
    }
}

// refresh resource consumptions every x seconds
setInterval(() => {
    if(stationStore.selectedStation != null) {
        setChartDataByStationId(stationStore.selectedStation.stationId);
        // console.log(resourceStore);
    }
}, 5000);
