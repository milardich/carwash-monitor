import { getBoxInfo, type Box, type BoxInfo } from '@/api/box.api'
import { defineStore } from 'pinia';

export const useBoxStore = defineStore('box', {
  state: () => ({
    boxPopupOpen: false,
    selectedBox: undefined as Box | undefined,
    selectedBoxInfo: null as BoxInfo | null,
    refreshIntervalId: null as number | null
  }),
 
  actions: {
    setSelectedBox(box: Box) {
      this.selectedBox = box;
    },

    async loadBoxInfo(boxId: string) {
      this.selectedBoxInfo = await getBoxInfo(boxId);
    },

    async updateBoxInfo(box: Box) {
      this.selectedBoxInfo = await getBoxInfo(box.id);
    },

    toggleBoxPopup() {
      this.boxPopupOpen = !this.boxPopupOpen;
    },

    setBoxStatus(boxState: string) {
      if (this.selectedBox) {
        this.selectedBox.status = boxState;
      }
    },

    startAutoRefresh(boxId: string) {
      this.stopAutoRefresh();
      this.refreshIntervalId = window.setInterval(() => {
        this.loadBoxInfo(boxId);
      }, 60_000);
    },

    stopAutoRefresh() {
      if (this.refreshIntervalId !== null) {
        clearInterval(this.refreshIntervalId);
        this.refreshIntervalId = null;
      }
    }
  }
});