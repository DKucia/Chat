<template>
  <div class="container">
        <router-link to="/register">Register</router-link>
      <LoginForm @login-submit="loginSubmit"></LoginForm>
  </div>
</template>

<script>
import LoginForm from '../components/LoginForm';

export default {
  name: "Login",
  components:{
      LoginForm
  },
  methods:{
      loginSubmit(data){
          console.log(data);
          this.$router.push('/chat');
      }
  }

};
</script>

<style scoped>
.container{
    height: 80vh;
    display: flex;
    justify-content: center;
    align-items: center;
}
</style>