export async function load({ params }: { params: { userslug: string } }) {
	return {
		userslug: params.userslug
	};
}
