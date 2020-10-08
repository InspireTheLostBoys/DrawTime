<template>
  <v-app>
    <v-main>
      <v-toolbar color="primary" dense></v-toolbar>
      <router-view />
    </v-main>
  </v-app>
</template>

<script>

export default {
  name: 'App',

  components: {
  },

  data: () => ({
  }),
};
</script>
