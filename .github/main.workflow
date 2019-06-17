workflow "Build and deploy on push" {
  on = "push"
  resolves = ["GitHub Action for npm"]
}

action "Deploy To Liara" {
  uses = "ParsaGachkar/OpenChat/Deploy to Liara@builder"
}
