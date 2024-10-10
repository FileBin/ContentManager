import { type ClassValue, clsx } from 'clsx'
import { twMerge } from 'tailwind-merge'
import { array, NDArray } from 'vectorious'

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

export function one() {
  return array(
    new NDArray([1, 0, 0, 0, 1, 0, 0, 0, 1], {
      shape: [3, 3],
      dtype: 'float64'
    })
  )
}

export function scale2d(factor: number) {
  return array(
    new NDArray([factor, 0, 0, 0, factor, 0, 0, 0, 1], {
      shape: [3, 3],
      dtype: 'float64'
    })
  )
}

export function translate2d(x: number, y: number) {
  return array(
    new NDArray([1, 0, 0, 0, 1, 0, x, y, 1], {
      shape: [3, 3],
      dtype: 'float64'
    })
  )
}

export function rotate2d(angle: number) {
  const c = Math.cos(angle)
  const s = Math.sin(angle)
  return array(
    new NDArray([c, -s, 0, s, c, 0, 0, 0, 1], {
      shape: [3, 3],
      dtype: 'float64'
    })
  )
}
