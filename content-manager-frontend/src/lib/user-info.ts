export class UserInfo {
    name!: string;
    email!: string;

    static fromProfile(oidcProfile: Oidc.Profile): UserInfo {
        return {
            name: oidcProfile.name ?? "Anonymous",
            email: oidcProfile.email ?? "",
        }
    }
}