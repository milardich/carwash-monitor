import { getBoxInfo, type Box, type BoxInfo } from '@/api/box.api'
import { defineStore } from 'pinia';

export const useBoxStore = defineStore('box', {
    state: () => ({
        boxPopupOpen: false,
        selectedBox: undefined as Box | undefined,
        selectedBoxInfo: null as BoxInfo | null
    }),
    actions: {
        setSelectedBox(box: Box) {
            this.selectedBox = box;
        },
        async updateBoxInfo(box: Box){
            this.selectedBoxInfo = await getBoxInfo(
                box.id
            );
        },
        toggleBoxPopup() {
            this.boxPopupOpen = !this.boxPopupOpen;
        },
        setBoxStatus(boxState: string) {
            if (this.selectedBox) {
                this.selectedBox.status = boxState;
            }
        },
    },
});
