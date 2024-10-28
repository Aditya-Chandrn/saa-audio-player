<script>
	import { onMount } from 'svelte';
	import CookieStorage from '@/utils/cookiesManagement';
	import { goto } from '$app/navigation'; // Import goto for navigation

	onMount(() => {
		const user = CookieStorage.get('user');

		if (user == undefined) {
			goto('/account/login'); // Redirect to login
		} else {
			goto('/home'); // Redirect to music page
		}
	});
</script>
