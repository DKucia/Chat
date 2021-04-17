<template>
  <div class="container">
    <ErrorMessage :message="error" />
    <RegisterForm  @register-submit="onSubmit"/>
  </div>
</template>

<script>
import RegisterForm from "../components/RegisterForm";
import ErrorMessage from '../components/ErrorMessage';
import {mapActions} from 'vuex';

export default {
  name: "Register",
  components: {
    RegisterForm,
     ErrorMessage
  },
  data(){
    return{
      error:''
    }
  },
  methods:{
    ...mapActions(['signUp']),
    async onSubmit(data){
      console.log(data);
      try{
        await this.signUp(data);
        this.error='';
         this.$router.push('/login');
      }
      catch(e){
        this.error=e.response.data.error;
      }
    }
  }
};
</script>

<style>
.container {
  height: 80vh;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
</style>