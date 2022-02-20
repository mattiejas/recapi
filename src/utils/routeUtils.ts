export function removeLeadingAndTrailingSlashes(path: string): string {
  return path.replace(/^\/|\/$/g, '')
}
