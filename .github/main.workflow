workflow "Build and deploy on push" {
  on = "push"
  resolves = ["GitHub Action for npm"]
}

action "GitHub Action for npm" {
  uses = "ParsaGachkar/OpenChat/Deploy To Liara@builder"
}
