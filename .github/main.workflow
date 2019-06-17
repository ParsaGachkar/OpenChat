workflow "Build and deploy on push" {
  on = "push"
  resolves = ["Deploy To Liara"]
}

action "Deploy To Liara" {
  uses = "ParsaGachkar/OpenChat/Deploy to Liara@builder"
}
