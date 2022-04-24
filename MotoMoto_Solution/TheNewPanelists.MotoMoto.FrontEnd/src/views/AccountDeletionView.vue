<template>
  <div class="accountdelete">
    <AccountDelete username="username"/>
  </div>
</template>

<script>
import AccountDelete from '../components/AccountDeletionComponent.vue'

export default {
  name: 'AccountDeletionView',
  components: {
    AccountDelete
  },
}
</script>