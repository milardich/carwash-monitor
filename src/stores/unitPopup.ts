import { type Unit } from '@/api/unit.api'
import { defineStore } from 'pinia';

export const useUnitStore = defineStore('unit', {
    state: () => ({
        unitPopupOpen: false,
        selectedUnit: null as Unit | null
    }),
    actions: {
        setSelectedUnit(unit: Unit) {
            this.selectedUnit = unit;
        },
        toggleUnitPopup() {
            this.unitPopupOpen = !this.unitPopupOpen;
        }
    },
});
