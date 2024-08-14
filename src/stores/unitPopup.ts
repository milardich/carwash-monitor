import { getUnitInfo, type Unit, type UnitInfo } from '@/api/unit.api'
import { defineStore } from 'pinia';
import { useStationStore } from './stationStore';
import { strDateTime } from '@/util/dateTimeUtils';

export const useUnitStore = defineStore('unit', {
    state: () => ({
        unitPopupOpen: false,
        selectedUnit: undefined as Unit | undefined,
        selectedUnitInfo: null as UnitInfo | null,
        dateFrom: "" as string,
        dateTo: "" as string
    }),
    actions: {
        setSelectedUnit(unit: Unit) {
            this.selectedUnit = unit;
        },
        async updateUnitInfo(unit: Unit){
            this.selectedUnitInfo = await getUnitInfo(
                this.dateFrom,
                this.dateTo,
                useStationStore().selectedStation?.stationId,
                unit.unitId
            );
        },
        toggleUnitPopup() {
            this.unitPopupOpen = !this.unitPopupOpen;
        }
    },
});
