<template>
  <div class="container">
        <router-link to="/register">Register</router-link>
        <ErrorMessage :message="error" />
      <LoginForm @login-submit="loginSubmit"></LoginForm>
  </div>
</template>

<script>
import LoginForm from '../components/LoginForm';
import ErrorMessage from '../components/ErrorMessage';
import {mapActions} from 'vuex';

export default {
  name: "Login",
  components:{
      LoginForm,
      ErrorMessage
  },
  data(){
    return {
      error:''
    }
  },
  methods:{
    ...mapActions(["signIn"]),
      async loginSubmit(data){
          try{
            await this.signIn(data);
            this.error='';
            this.$router.push('/');
          }
          catch(e){
            this.error=e.response.data.error;
          }
      }
  }

};
</script>

<style scoped>
.container{
    height: 80vh;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
}
</style>