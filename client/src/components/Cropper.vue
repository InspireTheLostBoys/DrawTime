<template>
  <v-dialog v-model="dialog" persistent max-width="450">
    <v-card>
      <v-toolbar dense dark color="primary">
        <v-toolbar-title> Crop Image </v-toolbar-title>
      </v-toolbar>
      <v-card-text>
        <h3 class="mb-2">Confirm output image</h3>
        <div style="height: 450px;" v-if="dialog">
          <vue-cropper max-width="100%" :containerStyle="containerStyle" @ready="onCropperReady" dragMode="move"
            :aspectRatio="aspectRatio" :cropBoxResizable="true" ref="cropper" :src="imgSrc" alt="Source Image">
          </vue-cropper>
        </div>

        <!-- <div style="text-align: center">
          <img :src="imgExport" width="100%;" alt="" />
        </div> -->
      </v-card-text>
      <v-card-actions class="mx-2">
        <v-spacer></v-spacer>
        <v-btn @click="dialog = false">cancel </v-btn>
        <v-btn color="primary" @click="getCroppedCanvas"> Confirm </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
  import VueCropper from "vue-cropperjs";

  export default {
    components: {
      VueCropper,
    },
    data() {
      return {
        step: 1,
        params: null,
        imgSrc: "",
        imgExport: "",
        exportBlob: null,
        dialog: false,
        afterComplete: null,
        aspectRatio: null,
        containerStyle: {
          height: "400px",
        },
      };
    },
    computed: {

    },
    methods: {
      show(file, aspectRatio, afterComplete) {
        let self = this;
        self.aspectRatio = parseFloat(aspectRatio)
        self.afterComplete = afterComplete;

        self.dialog = true;

        setTimeout(() => {
          self.$nextTick(() => {
            const reader = new FileReader();

            reader.onload = (event) => {
              self.imgSrc = event.target.result;

              self.$refs.cropper.replace(event.target.result);
            };

            reader.readAsDataURL(file);
          });
        }, 200);
      },
      onCropperReady() {
        let self = this;
        var contData = self.$refs.cropper.getContainerData();
         //Get container data 
         self.$refs.cropper.setCropBoxData({ height: contData.height, width: contData.width }) 
         //set data
      },
      setDragMode(dragMode) {
        let self = this;
        self.$refs.cropper.setDragMode(dragMode);
      },
      getCroppedCanvas() {
        let self = this;
        let params
        if (self.aspectRatio == 1) {
          params = {
            height: 500,
            width: 500,
          };
        } else {
          params = {
            height: 720,
            width: 1280,
          };
        }


        self.imgExport = self.$refs.cropper.getCroppedCanvas().toDataURL();

        self.$refs.cropper.getCroppedCanvas(params).toBlob((blob) => {
          self.exportBlob = blob;


          var fr = new FileReader();

          fr.onload = function () {
            var img = new Image();
            img.onload = function () {};
            img.src = fr.result;
          };

          fr.readAsDataURL(blob);

          self.returnImageData(blob);
        });
      },
      returnImageData(blob) {
        let self = this;

        self.afterComplete(blob);

        self.dialog = false;
      },
    },
  };
</script>

<style>
  .controls {
    display: flex;
  }

  .control-item {
    width: 40px;
    height: 40px;
    background: rgb(199, 199, 199);
    border-bottom: 1px solid rgb(158, 158, 158);
    padding-top: 8px;
    text-align: center;
    border-radius: 5px;
    margin-right: 2px;
  }
</style>