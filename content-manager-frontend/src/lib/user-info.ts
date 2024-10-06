export class UserInfo {
    name!: string;
    email!: string;

    static fromProfile(oidcProfile: Oidc.Profile): UserInfo {
        return {
            name: oidcProfile.preferred_username ?? "Anonymous",
            email: oidcProfile.email ?? "",
        }
    }
}