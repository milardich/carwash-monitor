import { getBoxInfo, type Box, type BoxInfo } from '@/api/box.api'
import { defineStore } from 'pinia';
import { useStationStore } from './stationStore';
import { strDateTime } from '@/util/dateTimeUtils';

export const useUnitStore = defineStore('unit', {
    state: () => ({
        unitPopupOpen: false,
        selectedUnit: undefined as Box | undefined,
        selectedUnitInfo: null as BoxInfo | null,
        dateFrom: "" as string,
        dateTo: "" as string
    }),
    actions: {
        setSelectedUnit(unit: Box) {
            this.selectedUnit = unit;
        },
        async updateUnitInfo(unit: Box){
            this.selectedUnitInfo = await getBoxInfo(
                this.dateFrom,
                this.dateTo,
                useStationStore().selectedStation?.id,
                unit.id
            );
        },
        toggleUnitPopup() {
            this.unitPopupOpen = !this.unitPopupOpen;
        },
        setUnitStatus(unitState: string) {
            if (this.selectedUnit) {
                this.selectedUnit.status = unitState;
            }
        },
    },
});
