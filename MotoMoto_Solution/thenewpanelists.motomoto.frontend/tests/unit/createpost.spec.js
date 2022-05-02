import { shallowMount } from '@vue/test-utils'
import PostCreationComponent from '@/components/CommunityBoardComponents/PostComponents/PostCreationComponent.vue'

describe('PostCreationComponent.vue', () => {
  it('renders props.feedName when passed', () => {
    const feedName = 'Unit Test'
    const wrapper = shallowMount(PostCreationComponent, {
      props: { feedName }
    })
    expect(wrapper.props('feedName')).toBe(feedName)


    // Source: https://v1.test-utils.vuejs.org/api/wrapper/#setprops
    // const wrapper = mount(PostCreationComponent)
    // await wrapper.setProps({ feedName: 'Unit Test' })

    // expect(wrapper.vm.feedName).toBe('Unit Test')
  })
})
