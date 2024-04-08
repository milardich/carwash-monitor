import { reactive } from 'vue';
import { type Unit } from '@/api/unit.api'

export const unitPopup = reactive({
    isOpen: false,
    selectedUnit: null as Unit | null
});

export function setSelectedUnit(unit: Unit) {
    unitPopup.selectedUnit = unit;
}

export function toggleUnitPopup() {
    unitPopup.isOpen = !unitPopup.isOpen;
}