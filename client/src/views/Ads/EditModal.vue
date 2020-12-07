<template>
    <v-row justify="center">
        <v-dialog v-model="dialog" fullscreen persistent transition="dialog-bottom-transition">
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
                        <v-col class="pa-1" cols="12" lg="6" md="6">
                            <label>Description</label>
                            <v-text-field v-model="ad.description" dense outlined></v-text-field>
                        </v-col>
                        <v-col class="pa-1" cols="12" lg="6" md="6">
                            <label>Line 1</label>
                            <v-text-field v-model="ad.line1" dense outlined></v-text-field>
                        </v-col>
                        <v-col class="pa-1" cols="12" lg="6" md="6">
                            <label>Line 2</label>
                            <v-text-field v-model="ad.line2" dense outlined></v-text-field>
                        </v-col>
                        <v-col class="pa-1" cols="12" lg="6" md="6">
                            <label>Line 3</label>
                            <v-text-field v-model="ad.line3" dense outlined></v-text-field>
                        </v-col>
                        <v-col class="pa-1" cols="12" lg="6" md="6">
                            <label>Line 4</label>
                            <v-text-field v-model="ad.line4" dense outlined></v-text-field>
                        </v-col>
                        <v-col class="pa-1" cols="12" lg="6" md="6">
                            <label></label>
                            <v-checkbox label="use date range" v-model="useDateRange" @change="setDates()">

                            </v-checkbox>
                        </v-col>
                        <v-col v-if="useDateRange" class="pa-1" cols="12" lg="6" md="6">
                            <label>Show from</label>
                            <datetime :minute-step="15" type="datetime" v-model="ad.show_from" class="input">
                            </datetime>
                        </v-col>
                        <v-col v-if="useDateRange" class="pa-1" cols="12" lg="6" md="6">
                            <label>Show to</label>
                            <datetime :minute-step="15" type="datetime" v-model="ad.show_to" class="input">
                            </datetime>
                        </v-col>
                        <v-col cols="12" style="text-align: left;">
                            <label>Advert Image <v-btn icon color="error" @click="deleteIMG()">
                                    <v-icon>mdi-delete
                                    </v-icon>
                                </v-btn> </label>
                            <v-card v-if="showImage" height="200" width="355" @click="openfileTitleDialog">
                                <object height="200" width="100%" :data="getImg('Ads')" type="image/png">
                                    <img height="100%" src="img/no-image.png" style="width:100% !important">
                                </object>
                            </v-card>
                        </v-col>
                    </v-row>
                </v-container>
            </v-card>
            <YesNoModal ref="YesNoModal" />

            <Cropper ref="Cropper" />
            <input accept="image/*" ref="FileBackGround" @change="uploadtitleImage" type="file" style="display: none;">
        </v-dialog>
    </v-row>
</template>
<script>
    import axios from 'axios'
    import YesNoModal from "@/components/YesNoModal.vue"

    import Cropper from "@/components/Cropper.vue"
    import {
        Datetime
    } from 'vue-datetime';

    export default {
        components: {
            Datetime,
            Cropper,
            YesNoModal
        },
        data() {
            return {
                useDateRange: true,
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
            setDates() {
                let self = this
                if (self.useDateRange == false) {
                    self.ad.show_from = null
                    self.ad.show_to = null
                }
            },
            deleteIMG() {
                let self = this
                if (self.ad.ad_image != '' && self.ad.ad_image != null) {
                    self.showImage = false
                    self.$refs.YesNoModal.show("error", "Delete Advert",
                        "Are you sure you want to delete this ad image?",
                        afterRuturn => {
                            if (afterRuturn) {
                                axios.delete(process.env.VUE_APP_IMAGE_SERVER_ADDRESS + 'delete?path=DrawTime/' +
                                        "Ads" + '/' + self.ad.id)
                                    .then(r => {
                                        console.log(r.data);
                                        self.ad.ad_image = ''
                                        self.put(`playlists`, self.ad).then(r => {})
                                        self.showImage = true
                                    }).catch(e => {
                                        alert("error deleteing image " + e.toString())
                                        self.showImage = true
                                    })
                            } else {
                                self.showImage = true
                            }
                        })
                }
            },
            validate() {
                let self = this
                if (self.useDateRange) {
                    self.ad.show_from = self.FormatDateTimeGMT(self.ad.show_from)
                    self.ad.show_to = self.FormatDateTimeGMT(self.ad.show_to)
                }
                self.put(`playlists`, self.ad).then(r => {
                    self.dialog = false
                    self.callback()
                })

            },
            uploadtitleImage(e) {
                let self = this;
                let file = e.target.files[0];
                self.$refs.Cropper.show(file, 1.7777777777, afterComplete => {
                    const formData = new FormData();
                    self.showImage = false
                    formData.append('file', afterComplete)
                    const config = {
                        headers: {
                            'content-type': 'multipart/form-data'
                        }
                    }
                    axios.post(process.env.VUE_APP_IMAGE_SERVER_ADDRESS + 'upload?path=DrawTime/Ads/' + self
                            .ad.id, formData, config)
                        .then(r => {
                            console.log(r.data);
                            self.ad.ad_image = self.ad.id + '.png'
                            self.put(`playlists`, self.ad).then(r => {
                            self.showImage = true
                            })
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