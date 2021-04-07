<template>
  <div class="container">
    <RegisterForm />
  </div>
</template>

<script>
import RegisterForm from "../components/RegisterForm";

export default {
  name: "Register",
  components: {
    RegisterForm,
  },
};
</script>

<style>
.container {
  height: 80vh;
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>