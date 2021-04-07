<template>
  <div>
    <h1>Login</h1>
    <label for="email">Email </label>
    <input type="text" name="email" v-model="email" />
    <label for="password">Password</label>
    <input type="password" name="password" v-model="password" />
    <button @click="onSubmit">Login</button>
  </div>
</template>

<script>
export default {
  name: "LoginForm",
  data() {
    return {
      email: "",
      password: "",
    };
  },
  methods: {
    onSubmit() {
      this.$emit("login-submit", {
        email: this.email,
        password: this.password,
      });
    },
  },
};
</script>

<style scoped>
div {
  width: 500px;
  align-items: center;
  justify-content: center;
  display: flex;
  flex-direction: column;
}
</style>