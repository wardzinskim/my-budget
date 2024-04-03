import styles from './styles.module.css';

interface ButtonProps {
  title: string;
}

export function Button(props: ButtonProps) {
  return <button className={`${styles.button}`}>{props.title}</button>;
}
