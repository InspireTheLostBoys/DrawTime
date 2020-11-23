<template>
    <v-row justify="center">
        <v-dialog v-model="dialog" persistent transition="dialog-bottom-transition">
            <v-card tile>
                <v-toolbar dark color="primary" flat>
                    <v-btn icon dark @click="dialog = false">
                        <v-icon>mdi-close</v-icon>
                    </v-btn>
                    <v-spacer></v-spacer>
                    <v-toolbar-items>
                        <v-btn dark text @click="validate()">
                            Save
                        </v-btn>
                    </v-toolbar-items>
                </v-toolbar>
                <v-container fluid class="fill-height">
                    <v-row no-gutters>
                        <v-col cols="12">
                            <label>Description</label>
                            <v-text-field v-model="ad.description" dense outlined></v-text-field>
                        </v-col>
                        <v-col cols="12">
                            <label>Line 1</label>
                            <v-text-field v-model="ad.line1" dense outlined></v-text-field>
                        </v-col>
                        <v-col cols="12">
                            <label>Line 2</label>
                            <v-text-field v-model="ad.line2" dense outlined></v-text-field>
                        </v-col>
                        <v-col cols="12">
                            <label>Line 3</label>
                            <v-text-field v-model="ad.line3" dense outlined></v-text-field>
                        </v-col>
                        <v-col cols="12">
                            <label>Line 4</label>
                            <v-text-field v-model="ad.line4" dense outlined></v-text-field>
                        </v-col>
                        <v-col cols="12">
                            <label>Show from</label>
                            <datetime type="datetime" v-model="ad.show_from" class="input">
                            </datetime>
                        </v-col>
                        <v-col cols="12">
                            <label>Show to</label>
                            <datetime type="datetime" v-model="ad.show_to" class="input">
                            </datetime>
                        </v-col>
                        <v-col cols="12" style="text-align: left;">
                            <label>AD Image <v-btn icon color="error" @click="deleteIMG('DrawTime/Ads/')">
                                    <v-icon>mdi-delete
                                    </v-icon>
                                </v-btn> </label>
                            <v-card v-if="showImage" height="250" width="250" @click="openfileTitleDialog">
                                <object height="250" width="250" :data="getImg('Ads')" type="image/png">
                                    <img height="100%" src="img/no-image.png" style="width:100% !important">
                                </object>
                            </v-card>
                        </v-col>
                    </v-row>
                </v-container>
            </v-card>
            <Cropper ref="Cropper" />
            <input accept="image/*" ref="FileBackGround" @change="uploadtitleImage" type="file" style="display: none;">
        </v-dialog>
    </v-row>
</template>
<script>
    import axios from 'axios'
    import Cropper from "@/components/Cropper.vue"
    import {
        Datetime
    } from 'vue-datetime';

    export default {
        components: {
            Datetime,
            Cropper
        },
        data() {
            return {
                dialog: false,
                showImage: true,
                ad: {
                    id: 0,
                    description: "",
                    line1: "",
                    line2: "",
                    line3: "",
                    line4: "",
                    show_from: null,
                    show_to: null,
                },
                callback: null,

            }
        },
        methods: {
            deleteIMG(directory) {
                let self = this
                self.showImage = false
                axios.delete(process.env.VUE_APP_IMAGE_SERVER_ADDRESS + 'delete/' + directory + '/' + self.draw.id)
                    .then(r => {
                        console.log(r.data);
                        self.ad.ad_image = ''
                        self.put(`dt_ads`, self.ad).then(r => {})
                        self.showImage = true
                    })
            },
            validate() {
                let self = this
                self.put(`dt_ads`, self.ad).then(r => {
                    self.dialog = false
                    self.callback()
                })

            },
            uploadtitleImage(e) {
                let self = this;
                let file = e.target.files[0];
                self.$refs.Cropper.show(file, null, afterComplete => {
                    const formData = new FormData();
                    self.showImage = false
                    formData.append('file', afterComplete)
                    const config = {
                        headers: {
                            'content-type': 'multipart/form-data'
                        }
                    }
                    axios.post(process.env.VUE_APP_IMAGE_SERVER_ADDRESS + 'upload?path=DrawTime/adverts/' + self
                            .ad.id, formData, config)
                        .then(r => {
                            console.log(r.data);
                            ad.ad_image = 'DrawTime/adverts/' + self.ad.id
                            self.put(`dt_ads`, self.ad).then(r => {})
                            self.showImage = true
                        })
                })
            },
            getImg(directory) {
                let self = this
                return process.env.VUE_APP_IMAGE_SERVER_ADDRESS + "DrawTime/" + directory + '/' + self.ad.id + ".png"
            },
            openfileTitleDialog() {
                let self = this;
                self.$refs.FileBackGround.value = null;
                self.$refs.FileBackGround.click();
            },
            open(item, callback) {
                let self = this
                for (var prop in item) {
                    self.ad[prop] = item[prop]
                }
                self.dialog = true
                self.callback = callback
            }
        }
    }
</script>

<style scoped>
    .input {
        height: 40px;
        width: 100%;
        border: 1px solid #9e9e9e;
        border-radius: 5px;
        padding-left: 10px;
        padding-top: 7px;
        margin-bottom: 20px;
    }
</style>