<template>
  <div>
    <h1>Register</h1>
    <label for="email">Email </label>
    <input type="text" name="email" v-model="email" />
    <label for="username">Username</label>
    <input type="text" name="username" v-model="username" />
    <label for="password">Password</label>
    <input type="password" name="password" v-model="password" />
    <label for="repeatPassword">Repeat password</label>
    <input type="password" name="repeatPassword" v-model="repeatPassword" />

    <button @click="onSubmit">Register</button>
  </div>
</template>

<script>
export default {
  data() {
    return {
      email: "",
      password: "",
      repeatPassword: "",
      username: "",
    };
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