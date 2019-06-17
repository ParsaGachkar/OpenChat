workflow "Build and deploy on push" {
  on = "push"
  resolves = ["Deploy To Liara","Build Android"]
}

action "Deploy To Liara" {
  uses = "./."
  secrets = ["LIARA_IR_API_KEY"]
}

action "Build Android" {
  uses = "./Mobile/android.dockerfile"
}
