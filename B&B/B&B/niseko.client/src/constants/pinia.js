//2. @/constants/pinia.js
//set pinia (just for temporary storage �ȼȦs) and pinia-plugin-persistedstate (for �x�s���A���[��)
import { createPinia } from 'pinia';
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate';
const pinia = createPinia();
pinia.use(piniaPluginPersistedstate);
export default pinia;

//�p�G piniaPluginPersistedstate ����ȥΩ��{�ɦs�x�]�p�ƥ����ƾڡ^�A�i�H�N�����νd�򭭨�b�S�w�� Store ���A�Ӥ��O�����ϥΡC�o�˥i�H�קK�����n���ʯ�}�P�C
