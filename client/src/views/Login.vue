<template>
  <v-app>
    <v-content>
      <v-img src="img/login.jpg" style="height: 100vh">
        <v-container fluid class="fill-height" style="background: transparent; height: 100vh">
          <v-row justify="center">
            <v-col cols="6" v-if="reset">
              <v-form @submit.prevent="sendResetPassword">
                <h1>Forgotten your password?</h1>
                <p class="mt-3" style="font-size: 16px !important">
                  Don't worry, we'll send you a message to help you reset your
                  password.
                </p>
                <v-text-field class="mt-4" prepend-inner-icon="mdi-email" dense placeholder="Email" outlined
                  v-model="email" :rules="emailRules" required></v-text-field>
                <v-btn x-large :loading="loading" type="submit" dark tile block class="ml-0 mb-5" color="#fa41cc">
                  Continue
                </v-btn>
                <div class="mb-6">
                  <a style="color: #fa41cc" href="#" @click.prevent="reset = false">Return to log in</a>
                </div>
              </v-form>
            </v-col>
            <v-col cols="10" v-if="!reset" style="margin-top: 200px;">
              <v-form @submit.prevent="login" ref="form" v-model="valid" lazy-validation>
                <v-text-field prepend-inner-icon="mdi-email" dark dense placeholder="User ID" filled v-model="uid"
                  required>
                </v-text-field>

                <v-text-field prepend-inner-icon="mdi-lock" dark dense placeholder="Pin" filled v-model="pin" required>
                </v-text-field>

                <v-btn :loading="loading" type="submit" dark tile block class="ml-0 mb-5" color="yellow">Sign in
                </v-btn>

                <!-- <v-btn color="#f0d9e3" x-large @click="$router.push('/register')" block tile
                              class="ml-0 mb-5 elevation-2">Register Vendor</v-btn>-->

                <!-- <div class="text-center mb-6">
                <a style="color: #fa41cc" href="#" @click.prevent="reset = true">Forgot password?</a>
              </div>

              <div class="text-center mb-6">
                <a style="color: #fa41cc" href="#" @click.prevent="goToRegistration">Sign Up</a>
              </div> -->

                <div class="text-center">
                  <!-- <v-btn @click="googleLogin" class="ml-0" tile
                                                                        color="secondary">
                                                                        <v-icon>mdi-google</v-icon>
                                                                    </v-btn>
                                                                    <v-btn class="ml-1" tile color="primary">
                                                                        <v-icon>mdi-facebook</v-icon>
                                </v-btn>-->
                </div>
              </v-form>
            </v-col>
          </v-row>
        </v-container>
      </v-img>
    </v-content>
    <ErrorDialog ref="error" />
  </v-app>
</template>

<script>
  import Axios from "axios";
  import ErrorDialog from "../components/Common/Overlays/Dialogs/Error";

  export default {
    components: {
      ErrorDialog,
    },
    created() {},
    data: () => ({
      reset: false,
      loading: false,
      valid: true,
      uid: "",
      pin: "",
    }),
    methods: {
      login() {
        let self = this;

        if (this.$refs.form.validate()) {

          setTimeout(() => {
            let request = {
              uid: self.uid,
              pin: self.pin
            };

            Axios.post(process.env.VUE_APP_API + "user/Login", request)
              .then((r) => {
                console.log(r.data);
                if (r.data.user_id != 0) {
                  localStorage.setItem("userDetails", JSON.stringify({
                    user_id: r.data.user_id,
                    role_id: r.data.role_id
                  }));
                  self.$router.push("/Landing")
                } else {
                  let err = e.response.data;
                  self.$refs.error.show(err.error_description);
                }
              })
              .catch((e) => {
                let err = e.response.data;
                self.$refs.error.show(err.error_description);
              });
          }, 500);
        }
      },
      sendResetPassword() {
        let self = this;
      },
      googleLogin() {
        let self = this;
        window.location.href =
          "/api/Account/ExternalLogin?provider=Google&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A8800%2FLogin&state=j7NaZEjcc4XwJKiHy9FL2DkbsJLuwbGRFqNtdgwqElg1";
      },
      goToRegistration() {
        this.$router.push(`/Registration`);
      },
    },
  };
</script>