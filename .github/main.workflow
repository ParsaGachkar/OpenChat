workflow "Build and deploy on push" {
  on = "push"
  resolves = ["Deploy To Liara"]
}

action "Deploy To Liara" {
  uses = "./."
  secrets = ["LIARA_IR_API_KEY"]
}
